using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable, DisplayName("AllowInput Marker")]
public class AllowInputMarker : Marker, INotification
{
    //public AllowedInput allowedInput = AllowedInput.None;

    public PropertyName id => new PropertyName();
}
