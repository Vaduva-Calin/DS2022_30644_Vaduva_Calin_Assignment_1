import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import {trigger, state, style, animate, transition} from '@angular/animations';
import { User } from '../models/user';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.scss']
})
export class UserDialogComponent implements OnInit {
user = <User>{};
testGroup: FormGroup = new FormGroup({
  qty: new FormControl(['']),
});

  constructor(
    public dialogRef: MatDialogRef<UserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {

    if(data){
        console.log('am primit data');
        console.log(data);
        this.user.userName=data.username;
        this.user.password=data.password;
        this.user.name=data.name;
      }

  }

  ngOnInit(): void {
  }

  onCancel(): void {
    this.dialogRef.close();
  }

}
