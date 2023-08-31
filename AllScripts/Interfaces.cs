    using UnityEngine;

    public interface IMove
    {
        public void Move();
    }

    public interface ITrigger
    {
        public void OnTriggerEnter(Collider other);
    }