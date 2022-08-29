using UnityEngine;
using TMPro;
using DG.Tweening;

namespace ScoreCounting
{
    public class ScoreCounter : ConfigReceiver
    {
        private int _score;
        private TMP_Text _scoreView;
        private readonly float _punchDuration = 0.25f;

        private void Awake() => _scoreView = FindObjectOfType<Score>().GetComponentInChildren<TMP_Text>();

        public void Add()
        {
            _score++;
            Display();
        }

        public void Reset()
        {
            _score = 0;
            Display();
        }

        private void Display()
        {
            _scoreView.text = _score.ToString();
            _scoreView.rectTransform.DOPunchScale(Vector3.one * _config.Punch, _punchDuration, 0, 0);
        }
    }
}