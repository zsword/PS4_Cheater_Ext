// Decompiled with JetBrains decompiler
// Type: debugwatch.AssemblerView
// Assembly: dbgw, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C42DB83B-FBD0-4471-97D2-F43102A97A5F
// Assembly location: C:\Users\x\Downloads\PS4\debugwatch\dbgw\dbgw.exe

using KeystoneNET;
using System;
using System.Windows.Forms;

namespace debugwatch
{
  partial class AssemblerView : Form
  {
    private ulong _address;
    private byte[] result;

    public AssemblerView(ulong address)
    {
      this.InitializeComponent();
      this._address = address;
    }

    public byte[] GetAssemblerResult()
    {
      return this.result;
    }

    private void AssembleButton_Click(object sender, EventArgs e)
    {
      using (Keystone keystone = new Keystone(KeystoneArchitecture.KS_ARCH_X86, KeystoneMode.KS_MODE_MIPS64, true))
        this.result = keystone.Assemble(this.AssemblerTextBox.Text, this._address).Buffer;
      this.Close();
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
