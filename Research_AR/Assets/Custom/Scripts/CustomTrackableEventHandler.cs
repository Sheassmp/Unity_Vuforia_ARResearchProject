﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class CustomTrackableEventHandler : DefaultTrackableEventHandler {
        public static CustomTrackableEventHandler instance;
     
        private void Awake() {
            instance = this;
        }
     
        protected override void OnTrackingFound() {
            base.OnTrackingFound();
     
            Debug.LogError("<color=green>Tracking found!</color>");
     
            Events.TrackingFound();
        }
     
        protected override void OnTrackingLost() {
            base.OnTrackingLost();
     
            Debug.LogError("<color=red>Tracking lost!</color>");
     
            Events.TrackingLost();
        }
    }