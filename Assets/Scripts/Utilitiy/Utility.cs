using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Utility
{
    public static class Random
    {
        public static List<Vector3> GetRandomPointInBounds(int _positionCounts, BoundsInt _area)
        {
            var _randomPositions = new List<Vector3>();
				
            for (int _i = 0; _i < _positionCounts; _i++)
            {
                Vector3 _centPos = _area.position;
                Vector3 _size    = _area.size;

                float _x = UnityEngine.Random.Range(_centPos.x - _size.x/2, _centPos.x + _size.x/2);
                float _y = _centPos.y;
                float _z = UnityEngine.Random.Range(_centPos.z - _size.z/2, _centPos.z + _size.z/2);

                var _randomPos = new Vector3(_x, _y, _z);
                
                _randomPositions.Add(_randomPos);
            }

            return _randomPositions;
        }
    }
}