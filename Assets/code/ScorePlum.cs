using UnityEngine;
using System.Collections;

public class ScorePlum : MonoBehaviour
{
    #region Fields
    public float moveSpeed;
    public float fadeSpeed;

    // This is now my calling card.
    private Transform mTransform;
    private TextMesh  mText;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        mTransform = this.transform;
        mText = GetComponent<TextMesh>();
    }

    private void Update()
    {
        mTransform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        Color colour = mText.color;
        colour.a -= fadeSpeed * Time.deltaTime;

        mText.color = colour;

        if (mText.color.a <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
