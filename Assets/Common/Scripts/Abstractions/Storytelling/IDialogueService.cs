using System;
using System.Diagnostics;

namespace KarenKrill.Storytelling.Abstractions
{
    public interface IDialogueService
    {
        public event Action<int, int, float> ClientServed; // ������� ����� �� ������ ���� ������ ���������� �������, �� ����� �����
        void StartDialogue(int id);
        void MakeDialogueChoice(int index);
        void NextDialogueLine();
        void SkipDialogue();
        void SetVariable(string name, bool state = true);
    }
}
