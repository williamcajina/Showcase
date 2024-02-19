using SphereFitting;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmDevice : MonoBehaviour
{
    private const float MAX_DISTANCE = 0.5f;
    private const int MAX_RECORD = 50;
    private readonly List<Point> _record = new();

    public FitterManager FitterManager;
    public string Address { get; set; }
    public float LatestInverseAspectRatio { get; private set; } = 0f;
    public int MaxSignalStrength { get; private set; } = -120;
    public int MinSignalStrength { get; private set; } = -1;
    public string SSID { get; set; }
    private void Awake() => FitterManager.Points = _record;

    private void CheckSignal(int signalStr)
    {
        bool changed = signalStr < MaxSignalStrength || signalStr > MinSignalStrength;
        if (signalStr < MaxSignalStrength)
        {
            MaxSignalStrength = signalStr;
        }

        if (signalStr > MinSignalStrength)
        {
            MinSignalStrength = signalStr;
        }

        if (!changed) return;

        foreach (Point pointRecord in _record)
        {
            pointRecord.InverseAspectRatio = Helpers.CalculateInverseAspectRatio(MaxSignalStrength, MinSignalStrength, pointRecord.SignalStrength);
        }
    }

    public void AddPoint(Vector3 position, int _signalStrenght)
    {
        Debug.LogFormat("Adding point at position ({0:F2}, {1:F2}, {2:F2}) with RSSI {3}.", position.x, position.y, position.z, _signalStrenght);
        CheckSignal(_signalStrenght);

        Point point = new(position, Helpers.CalculateInverseAspectRatio(MaxSignalStrength, MinSignalStrength, _signalStrenght), _signalStrenght);
        Debug.LogFormat("Checking point {0}", point.ToString());
        LatestInverseAspectRatio = point.InverseAspectRatio;

        if (_record.Any() && (point.Position == _record.Last().Position))
        {
            Debug.LogWarning("Duplicate point detected. Ignoring point.");
            return;
        }

        List<Point> similarPoints = _record.Where(p => p.SignalStrength == point.SignalStrength && Vector3.Distance(p.Position, point.Position) < MAX_DISTANCE).ToList();
        if (similarPoints.Count > 0)
        {
            Point combinedPoint = new(similarPoints.Aggregate(point.Position, (acc, p) => acc + p.Position) / (similarPoints.Count + 1), point.InverseAspectRatio, _signalStrenght);
            _=_record.RemoveAll(p => p.SignalStrength == point.SignalStrength && Vector3.Distance(p.Position, point.Position) < MAX_DISTANCE);
            _record.Add(combinedPoint);
            Debug.LogFormat("Combined {0} points with RSSI {1} into new point {2}.", similarPoints.Count + 1, _signalStrenght, combinedPoint.ToString());
        }

        if (_record.Count < MAX_RECORD)
        {
            _record.Add(point);
            Debug.LogFormat("Point add ed. Record count: {0}.", _record.Count);
        }
        else if (_record.Count > MAX_RECORD)
        {
            float min = _record.Min(p => p.InverseAspectRatio);
            if (point.InverseAspectRatio > min)
            {
                int pointsRemoved = _record.RemoveAll(p => p.InverseAspectRatio == min);
                Debug.LogWarningFormat("Maximum point count reached. Removed {0} points with the lowest inverse aspect ratio.", pointsRemoved);
            }
        }
    }
}