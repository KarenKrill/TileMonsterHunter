using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace TileMonsterHunter
{
    using Abstractions;
    using Input.Abstractions;

    public class TileFieldSpawner : MonoBehaviour, ITileFieldSpawner
    {
        public event System.Action SpawnCompleted;

        [Inject]
        public void Initialize(IInputActionService inputActionService)
        {
            _inputActionService = inputActionService;
        }
        public void Spawn(ITileField tileField)
        {
            if (_tileField != null)
            {
                _tileField.TileRemoved -= OnTileRemoved;
            }
            _tileField = tileField;
            _tileField.TileRemoved += OnTileRemoved;
            StopAllCoroutines();
            StartCoroutine(SpawnCoroutine());
        }

        [SerializeField]
        private GameObject _tilesPrefab;
        [SerializeField]
        private float _spawnSpacing = 1;
        [SerializeField]
        private Transform _spawnTransform;

        private readonly Dictionary<GameObject, int> _tiles = new();
        private ITileField _tileField;
        private IInputActionService _inputActionService;

        private void OnEnable()
        {
            _inputActionService.Attack += ClickPerformed;
        }
        private void OnDisable()
        {
            _inputActionService.Attack -= ClickPerformed;
        }
        private void OnTileRemoved(TileInfo tileInfo)
        {
            var tilePair = _tiles.First(tile => tile.Value == tileInfo.ID);
            tilePair.Key.SetActive(false);
            _tiles.Remove(tilePair.Key);
        }
        private IEnumerator SpawnCoroutine()
        {
            if (_tiles.Count > 0)
            {
                foreach (var tile in _tiles.Keys)
                {
                    tile.SetActive(false);
                    Destroy(tile);
                }
                _tiles.Clear();
            }
            for (int i = 0; i < _tileField.Tiles.Count; i++)
            {
                Collider2D collider2D;
                Vector2 spanPosition = _spawnTransform.position;
                spanPosition.x += Random.Range(-_spawnSpacing / 2, _spawnSpacing / 2);
                spanPosition.y += Random.Range(-_spawnSpacing / 2, _spawnSpacing / 2);
                do
                {
                    yield return null;
                    collider2D = Physics2D.OverlapCircle(spanPosition, _spawnSpacing / 2);
                }
                while (collider2D != null && collider2D.TryGetComponent<Tile>(out _));

                var tileInfo = _tileField.Tiles[i];
                var tileGO = Instantiate(_tilesPrefab, _spawnTransform);
                _tiles[tileGO] = tileInfo.ID;
                var tileRigidbody = tileGO.GetComponent<Rigidbody2D>();
                tileRigidbody.sharedMaterial = tileInfo.PhysicsMaterial;
                tileRigidbody.mass = tileInfo.Mass;
                tileRigidbody.AddForce(Vector2.down);
                var tile = tileGO.GetComponent<Tile>();
                tile.ShapeSprite = tileInfo.ShapeSprite;
                tile.FaceSprite = tileInfo.FaceSprite;
                tile.Color = tileInfo.Color;
            }
            SpawnCompleted?.Invoke();
        }
        private void ClickPerformed()
        {
            if (Pointer.current != null)
            {
                var screenPosition = Pointer.current.position.ReadValue();
                var ray = Camera.main.ScreenPointToRay(screenPosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.transform != null && hit.transform.gameObject.TryGetComponent<Tile>(out var tile))
                {
                    _tileField.ChooseTile(_tiles[tile.gameObject]);
                }
            }
        }
    }
}
