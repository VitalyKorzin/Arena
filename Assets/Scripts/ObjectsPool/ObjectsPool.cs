using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectsPool<TPoolObject> : MonoBehaviour
    where TPoolObject: PoolObject
{
    [SerializeField] private TPoolObject[] _templates;

    private IEnumerable<TPoolObject> _deactivatedObjects;
    private List<TPoolObject> _pool;
    private Transform _container;
    private TPoolObject _createdClone;
    private int _randomIndex;

    protected virtual void OnEnable() => Validate();

    protected virtual void Initialize(Transform container)
    {
        _pool = new List<TPoolObject>();
        _container = container;
        Fill();
    }

    protected bool TryGetRandomObject(out TPoolObject poolObject)
    {
        _deactivatedObjects = _pool.Where(desiredObject => desiredObject.Deactivated);
        _randomIndex = UnityEngine.Random.Range(0, _deactivatedObjects.Count());
        poolObject = _deactivatedObjects.ElementAtOrDefault(_randomIndex);
        return poolObject != null;
    }

    private void Fill()
    {
        foreach (var template in _templates)
            for (var i = 0; i < template.ClonesCount; i++)
                CreateClone(template);
    }

    private void CreateClone(TPoolObject template)
    {
        _createdClone = Instantiate(template, _container);
        _createdClone.Deactivate();
        _pool.Add(_createdClone);
    }

    protected virtual void Validate()
    {
        if (_templates == null)
            throw new InvalidOperationException();

        if (_templates.Length == 0)
            throw new InvalidOperationException();

        foreach (var template in _templates)
            if (template == null)
                throw new InvalidOperationException();
    }
}