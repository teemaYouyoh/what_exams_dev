using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordsTableSceneScript : MonoBehaviour
{
    public GameObject RecordsTable;

    public Animation Menu;
    public Animation Info;
    public Animation Title;
    public Animation RecordsTableAnim;
    public Animation BackBttn;
    public Animation WhitePanel;

    // Start is called before the first frame update
    void Start()
    {
        WhitePanel.Play("WhitePanelFadeOut");
        Title.Play("RecordTableTitleFadeDown");
        Menu.Play("RecordTableMenuFadeRight");
        Info.Play("RecordTableRecordFadeLeft");
    }

    public void onRecordsClick()
    {
        RecordsTableAnim.Play("RecordTableRecordsTableFadeUp");
        BackBttn.Play("RecordTableTitleFadeDown");
        Title.Play("RecordTableTitleFadeTop");
        Menu.Play("RecordTableMenuFadeLeft");
        Info.Play("RecordTableRecordFadeRight");
    }

    public void onBackClick()
    {
        RecordsTableAnim.Play("RecordTableRecordsTableFadeDown");
        BackBttn.Play("RecordTableTitleFadeTop");
        Title.Play("RecordTableTitleFadeDown");
        Menu.Play("RecordTableMenuFadeRight");
        Info.Play("RecordTableRecordFadeLeft");
    }

    public void onClickExit()
    {
        Title.Play("RecordTableTitleFadeTop");
        Menu.Play("RecordTableMenuFadeLeft");
        Info.Play("RecordTableRecordFadeRight");
        WhitePanel.Play("WhitePanelFadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
