using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public interface IRestartable
{
    void Restart();
}

public struct ObjectPosition
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public ObjectPosition(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    public void SetPosition(Transform transform)
    {
        transform.position = Position;
        transform.rotation = Rotation;
        transform.localScale = Scale;
    }
}

public class Restarter : Singleton<Restarter>
{
    private readonly Dictionary<MonoBehaviour, ObjectPosition> items = new Dictionary<MonoBehaviour, ObjectPosition>();

    public void Register<T>(T item) where T : MonoBehaviour, IRestartable
    {
        if (!items.ContainsKey(item))
            items.Add(item, new ObjectPosition(item.transform.position, item.transform.rotation, item.transform.localScale));
    }

    private void Awake() => SceneManager.sceneUnloaded += Clear;

    private void OnEnable() => Events.OnRestart.AddListener(Restart);

    private void OnDisable() => Events.OnRestart.RemoveListener(Restart);

    private void Restart()
    {
        foreach (KeyValuePair<MonoBehaviour, ObjectPosition> item in items)
        {
            item.Key.StopAllCoroutines();
            item.Value.SetPosition(item.Key.transform);
            item.Key.GetComponent<IRestartable>().Restart();
        }
    }

    private void Clear<Scene>(Scene scene) => items.Clear();
}