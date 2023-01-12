import { SignalRService } from './../services/signal-r.service';
import { Energy } from './../models/energy';
import { EnergyService } from './../services/energy.service';
import { DeviceService } from './../services/device.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Device } from '../models/device';
import { DataSource } from '@angular/cdk/collections';
import { Observable, ReplaySubject } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTitleSubtitle,
  ApexStroke,
  ApexGrid
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  dataLabels: ApexDataLabels;
  grid: ApexGrid;
  stroke: ApexStroke;
  title: ApexTitleSubtitle;
};

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.scss']
})
export class UserPageComponent implements OnInit {
  userId: number=0;
  devices!: Device[];
  currentDevice!: Device;
  deviceColumns: string[] = ['description', 'address', 'maxConsumption', 'userId','calendar', 'daily consumption'];
  devicesToDisplay = this.devices;
  devicesSource = new ExampleDataSource(this.devicesToDisplay);
  energies?: Energy[] = [];
	model?: NgbDateStruct;
  id?: string;
  @ViewChild('chart') chart!: ChartComponent;
  chartOptions!: Partial<ChartOptions>;


  constructor(
    private router: Router,
    private jwtHelper: JwtHelperService,
    private route: ActivatedRoute,
    private deviceService: DeviceService,
    private energyService: EnergyService,
    private parserFormatter: NgbDateParserFormatter,
    private signalRService: SignalRService
    ) {
    this.generateChart();
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      let id=params.get('id');
      this.userId=+id!;
      console.log(this.userId);
      this.isUserAuthenticated();
      this.initiateWebSocket();
    })

  }

  initiateWebSocket(){
    this.signalRService.startConnection();
    this.signalRService.refreshChart();
    this.signalRService.notification();
    this.signalRService.refreshChart$.subscribe(()=> this.getChart(this.currentDevice));
  }

  isUserAuthenticated(){
    const token: string | null= localStorage.getItem("jwt");
    const role = localStorage.getItem('role');
    if(token && !this.jwtHelper.isTokenExpired(token) &&  role == 'user'){

      this.getUserDevices();
    }
    else{
      this.logOut();
    }
  }

  logOut(){
    localStorage.removeItem('jwt');
    localStorage.removeItem('role');
    this.router.navigate(['']);
  }

  getUserDevices(){
    this.deviceService.getUserDevices(this.userId).subscribe({
       next:(result :any) => {this.devicesSource.setData(result)},
       error: (err: HttpErrorResponse) => console.log(err)
    });
  }

  getChart(element:Device){
    this.currentDevice = element;
    var eConsum: number[] = [];
    let date = this.parserFormatter.format(this.model!);
    this.energyService.getEnergyForDevices(element.id!, date).subscribe({
      next:(result :any) => {
        let energies = result;
        for(let e of energies){
          eConsum.push(e.energyConsumption);
        }
        this.setChart(eConsum);
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
        this.emptyChart();
      }
    });

  }

  setChart(consumption:number[]){
   this.chartOptions.series=[
      {
        data: consumption
      }
    ];
  }

  emptyChart(){
    this.chartOptions.series=[
      {
        data: [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
      }
    ];
  }

  generateChart(){
    this.chartOptions = {
      series: [
        {
          name: "Wh",
          data: [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
        }
      ],
      chart: {
        height: 350,
        type: "line",
        zoom: {
          enabled: false
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "straight"
      },
      title: {
        text: "Consumption per day",
        align: "left"
      },
      grid: {
        row: {
          colors: ["#f3f3f3", "transparent"], // takes an array which will be repeated on columns
          opacity: 0.5
        }
      },
      xaxis: {
        categories: [
          "0:00",
          "1:00",
          "2:00",
          "3:00",
          "4:00",
          "5:00",
          "6:00",
          "7:00",
          "8:00",
          "9:00",
          "10:00",
          "11:00",
          "12:00",
          "13:00",
          "14:00",
          "15:00",
          "16:00",
          "17:00",
          "18:00",
          "19:00",
          "20:00",
          "21:00",
          "22:00",
          "23:00",
          "24:00",
        ]
      }
    };
  }

}

class ExampleDataSource extends DataSource<Object> {
  private _dataStream = new ReplaySubject<Object[]>();

  constructor(initialData: Object[]) {
    super();
    this.setData(initialData);
  }

  connect(): Observable<Object[]> {
    return this._dataStream;
  }

  disconnect() {}

  setData(data: Object[]) {
    this._dataStream.next(data);
  }
}
