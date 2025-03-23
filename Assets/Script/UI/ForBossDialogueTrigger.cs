using UnityEngine;

public class ForBossDialogueTrigger : MonoBehaviour
{
    
        public BossDialogueConfig bossDialogue;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                BossDialogueManager.Instance.StartBossDialogue(bossDialogue);
                GetComponent<Collider2D>().enabled = false;
            }
        }
    
}