using TMPro;

public class LoadingDisplayExample : LoadingDisplay
{
    public TMP_Text label;


    public override void Refresh(float percentage)
    {
        label.text = $"{percentage}%";
    }


    public override void Show()
    {
        transform.parent.gameObject.SetActive(true);
    }


    public override void Hide()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
