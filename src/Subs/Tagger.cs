using System;

namespace ElmForms.Subs
{
    public delegate T Tagger<out T>(EventArgs args);
}