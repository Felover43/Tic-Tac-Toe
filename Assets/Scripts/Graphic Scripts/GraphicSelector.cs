

using UnityEngine;


//stores the graphic for the game when they are loaded from the asset bundle.
//also contains default values if none have been loaded
public class GraphicSelector : MonoBehaviour
{


   public Sprite XGraphic = null;
   public Sprite OGraphic = null;
   public Sprite BackGround = null;





    public Sprite GetTokenGraphic(TokenState state) 
    { 
        return state==TokenState.X ? XGraphic : OGraphic;
    }
    public void SetGraphics(Sprite X, Sprite O , Sprite BG)
    {
        BackGround = BG;
        XGraphic = X;
        OGraphic = O;
    }
    public void UpdateBackground()
    {
        Object[] BGs = FindObjectsOfType(typeof(BackGround));
        BackGround bg=null;
        if(BGs != null)
        {
            for(int i = 0; i < BGs.Length; i++)
            {
                if (BGs[i] is BackGround)
                {
                    bg = (BackGround)BGs[i];
                    bg.SetBG(BackGround);
                  
                }
                    
            }
        }
    }
}
