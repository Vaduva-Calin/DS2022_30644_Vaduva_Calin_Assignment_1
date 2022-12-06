import { Device } from './../models/device';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormControl, FormGroup } from '@angular/forms';
import {trigger, state, style, animate, transition} from '@angular/animations';

@Component({
  selector: 'app-device-dialog',
  templateUrl: './device-dialog.component.html',
  styleUrls: ['./device-dialog.component.scss']
})
export class DeviceDialogComponent implements OnInit {
device = <Device>{};

  constructor(
    public dialogRef: MatDialogRef<DeviceDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    if(data){
      console.log('am primit data');
      console.log(data);
      this.device.description=data.description;
      this.device.address=data.address;
      this.device.maxConsumption=data.maxConsumption;
      this.device.userId=data.userId;
    }
  }

  ngOnInit(): void {
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
