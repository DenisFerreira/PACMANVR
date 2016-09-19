using UnityEngine;
using System.Collections.Generic;

public class CardboardMagnetSensor {

	private const int WINDOW_SIZE = 40;
	private const int NUM_SEGMENTS = 2;
	private const int SEGMENT_SIZE = WINDOW_SIZE / NUM_SEGMENTS;
	private const int T1 = 30, T2 = 130;

	// Variables:
	private static bool wasClicked;           // Flips to true once set off.
	private static bool sensorEnabled;        // Is sensor active.
	private static List<Vector3> sensorData;  // Keeps magnetic sensor data.
	private static float[] offsets;           // Offsets used to detect click.


	// Call this once at beginning to enable detection.
	public static void SetEnabled(bool enabled) {
		Reset();
		sensorEnabled = enabled;
		Input.compass.enabled = sensorEnabled;
	}

	// Reset variables.
	public static void Reset() {
		sensorData = new List<Vector3>(WINDOW_SIZE);
		offsets = new float[SEGMENT_SIZE];
		wasClicked = false;
		sensorEnabled = false;
	}

	// Poll this once every frame to detect when the magnet button was clicked
	// and if it was clicked make sure to call "ResetClick()"
	// after you've dealt with the action, or it will continue to return true.
	public static bool CheckIfWasClicked() {
		UpdateData();
		return wasClicked;
	}

	// Call this after you've dealt with a click operation.
	public static void ResetClick() {
		wasClicked = false;
	}

	// Updates 'sensorData' and determines if magnet was clicked.
	private static void UpdateData() {
		Vector3 currentVector = Input.compass.rawVector;

		if(sensorData.Count >= WINDOW_SIZE) sensorData.RemoveAt(0);
		sensorData.Add(currentVector);

		// Evaluate model:
		if(sensorData.Count < WINDOW_SIZE) return;

		float[] means = new float[2];
		float[] maximums = new float[2];
		float[] minimums = new float[2];

		Vector3 baseline = sensorData[sensorData.Count - 1];

		for(int i = 0; i < NUM_SEGMENTS; i++) {
			int segmentStart = 20 * i;
			offsets = ComputeOffsets(segmentStart, baseline);

			means[i] = ComputeMean(offsets);
			maximums[i] = ComputeMaximum(offsets);
			minimums[i] = ComputeMinimum(offsets);
		}

		float min1 = minimums[0];
		float max2 = maximums[1];

		// Determine if button was clicked.
		if(min1 < T1 && max2 > T2) {
			sensorData.Clear();
			wasClicked = true;  // Set button clicked to true.
			// NOTE: 'wasClicked' will now remain true until "ResetClick()" is called.
		}
	}

	private static float[] ComputeOffsets(int start, Vector3 baseline) {
		for(int i = 0; i < SEGMENT_SIZE; i++) {
			Vector3 point = sensorData[start + i];
			Vector3 o = new Vector3(point.x - baseline.x, point.y - baseline.y, point.z - baseline.z);
			offsets[i] = o.magnitude;
		}
		return offsets;
	}

	private static float ComputeMean(float[] offsets) {
		float sum = 0;
		foreach(float o in offsets) {
			sum += o;
		}
		return sum / offsets.Length;
	}

	private static float ComputeMaximum(float[] offsets) {
		float max = float.MinValue;
		foreach(float o in offsets) {
			max = Mathf.Max(o, max);
		}
		return max;
	}

	private static float ComputeMinimum(float[] offsets) {
		float min = float.MaxValue;
		foreach(float o in offsets) {
			min = Mathf.Min(o, min);
		}
		return min;
	}
}
