using Gameplay.Data;
using Infrastructure.FSM;
using Infrastructure.FSM.States;
using Infrastructure.SaveLoad;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MenuUI : MonoBehaviour
    {
        public Button continueButton;
        public Button startNewButton;
        public Button helpButton;
        public Button quitButton;
        private GameFSM _gameFSM;
        private UIFactory _uiFactory;
        private SaveLoadService _saveLoadService;

        [Inject]
        public void Construct(GameFSM gameFsm, UIFactory uiFactory, SaveLoadService saveLoadService)
        {
            _gameFSM = gameFsm;
            _uiFactory = uiFactory;
            _saveLoadService = saveLoadService;
        }
        

        private void Start()
        {
            continueButton.gameObject.SetActive(_saveLoadService.Load<ConstructorData>().IsValid());
            
            continueButton.OnClickAsObservable()
                .First()
                .Subscribe(_ => _gameFSM.Enter<Level>())
                .AddTo(this);
            
            startNewButton.OnClickAsObservable()
                .First()
                .Subscribe(_ => _gameFSM.Enter<StartNewLevel>())
                .AddTo(this);

            helpButton.OnClickAsObservable()
                .First()
                .Subscribe(_ => _uiFactory.Create<HelpWindow>(transform));

            quitButton.OnClickAsObservable()
                .First()
                .Subscribe(_ => Application.Quit());
        }
    }
}