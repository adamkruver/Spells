using UnityEngine;

namespace Sources.Frameworks.Mvp
{
    public class View<T> : MonoBehaviour, IView<T> where T : IPresenter
    {
        protected T Presenter { get; private set; }

        public void Construct(T presenter) => 
            Presenter = presenter ?? throw new System.ArgumentNullException(nameof(presenter));
    }
}