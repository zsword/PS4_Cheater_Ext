// Decompiled with JetBrains decompiler
// Type: debugwatch.MemoryMapView
// Assembly: dbgw, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C42DB83B-FBD0-4471-97D2-F43102A97A5F
// Assembly location: C:\Users\x\Downloads\PS4\debugwatch\dbgw\dbgw.exe

using libdebug;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace debugwatch
{
  partial class MemoryMapView : Form
  {
    private ProcessInfo processinfo;
    private ProcessMap processMap;
    private Button CloseButton;
    private CheckedListBox MemoryMapCheckedListBox;
    private Label HelpLabel;
    private Button AutoSelectButton;
    private Button ClearSelectButton;
    private TextBox SearchTextBox;
    private Label SearchLabel;

    public MemoryMapView(ProcessInfo pi, ProcessMap processMap)
    {
      InitializeComponent();
      this.processinfo = pi;
      this.processMap = processMap;
      


      for (int index = 0; index < this.processMap.entries.Length; ++index)
      {
        MemoryEntry entry = this.processMap.entries[index];
        ulong num = entry.end - entry.start;
        this.MemoryMapCheckedListBox.Items.Add((object) (entry.name +
                                                         " start: 0x" +
                                                         entry.start.ToString("X") +
                                                         " length: 0x" +
                                                         num.ToString("X") +
                                                         " prot: " +
                                                         (object) entry.prot));
      }
      this.AutoSelectButton_Click((object) null, (EventArgs) null);
    }

    public MemoryEntry[] GetSelectedEntries()
    {
      List<MemoryEntry> memoryEntryList = new List<MemoryEntry>();
      for (int index1 = 0; index1 < this.processMap.entries.Length; ++index1)
      {
        MemoryEntry entry = this.processMap.entries[index1];
        if (entry.name.Length != 0)
        {
          for (int index2 = 0; index2 < this.MemoryMapCheckedListBox.CheckedItems.Count; ++index2)
          {
            if (this.MemoryMapCheckedListBox.CheckedItems[index2].ToString().StartsWith(entry.name))
            {
              memoryEntryList.Add(entry);
              break;
            }
          }
        }
      }
      return memoryEntryList.ToArray();
    }

    private void AutoSelectButton_Click(object sender, EventArgs e)
    {
      string[] strArray = new string[3]
      {
        "executable",
        "anon:",
        "heap"
      };
      for (int index1 = 0; index1 < this.MemoryMapCheckedListBox.Items.Count; ++index1)
      {
        for (int index2 = 0; index2 < strArray.Length; ++index2)
        {
          if (this.MemoryMapCheckedListBox.Items[index1].ToString().ToLower().Contains(strArray[index2]))
          {
            this.MemoryMapCheckedListBox.SetItemChecked(index1, true);
            break;
          }
        }
      }
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void ClearSelectButton_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.MemoryMapCheckedListBox.Items.Count; ++index)
        this.MemoryMapCheckedListBox.SetItemChecked(index, false);
    }

    private void SearchTextBox_TextChanged(object sender, EventArgs e)
    {
      this.MemoryMapCheckedListBox.Items.Clear();
      for (int index = 0; index < this.processMap.entries.Length; ++index)
      {
        MemoryEntry entry = this.processMap.entries[index];
        ulong num = entry.end - entry.start;
        if (entry.name.Contains(this.SearchTextBox.Text))
          this.MemoryMapCheckedListBox.Items.Add((object) (entry.name + " start: 0x" + entry.start.ToString("X") + " length: 0x" + num.ToString("X") + " prot: " + (object) entry.prot));
      }
    }
  }
}
