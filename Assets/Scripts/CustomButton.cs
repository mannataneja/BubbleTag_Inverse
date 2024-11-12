using UnityEngine;
using UnityEngine.UI;

public class CustomButton : Button
{
    // Public method to expose the protected IsHighlighted() function
    public bool IsButtonHighlighted()
    {
        return IsHighlighted();
    }
}
