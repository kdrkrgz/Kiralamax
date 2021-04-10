import {  DataSource } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Observable, of as observableOf, merge } from 'rxjs';
import { map } from 'rxjs/operators';
import { Car } from 'src/app/Models/Car';
import { ListResponseModel } from 'src/app/Models/listResponseModel';
import { CarService } from 'src/app/Services/car.service';

export class CarDataSource extends DataSource<Car> {
    
  public data: Car[];
  paginator!: MatPaginator;
  sort!: MatSort;

  constructor(public carData: Car[]) {
    super();
    // carService.GetCars().subscribe(cars => {
    //   this.data = cars as Car[]
    // })
    this.data = carData as Car[];    
  }

  /**
   * Connect this data source to the table. The table will only update when
   * the returned stream emits new items.
   * @returns A stream of the items to be rendered.
   */
  connect(): Observable<Car[]> {
    // Combine everything that affects the rendered data into one update
    // stream for the data-table to consume.
    const dataMutations = [
      observableOf(this.data),
      this.paginator.page,
      this.sort.sortChange
    ];

    return merge(...dataMutations).pipe(map(() => {
      return this.getPagedData(this.getSortedData([...this.data]));
    }));
  }

  /**
   *  Called when the table is being destroyed. Use this function, to clean up
   * any open connections or free any held resources that were set up during connect.
   */
  disconnect() {}

  /**
   * Paginate the data (client-side). If you're using server-side pagination,
   * this would be replaced by requesting the appropriate data from the server.
   */
  private getPagedData(data: Car[]) {
    const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
    return data.splice(startIndex, this.paginator.pageSize);
  }

  /**
   * Sort the data (client-side). If you're using server-side sorting,
   * this would be replaced by requesting the appropriate data from the server.
   */
  private getSortedData(data: Car[]) {
    if (!this.sort.active || this.sort.direction === '') {
      return data;
    }

    return data.sort((a, b) => {
      const isAsc = this.sort.direction === 'asc';
      switch (this.sort.active) {
        case 'brand': return compare(a.brand, b.brand, isAsc);
        case 'category': return compare(a.category.categoryName, b.category.categoryName, isAsc);
        case 'id': return compare(+a.id, +b.id, isAsc);
        case 'carModel': return compare(+a.carModel, +b.carModel, isAsc);
        case 'color': return compare(+a.color, +b.color, isAsc);
        case 'gear': return compare(+a.gear, +b.gear, isAsc);
        case 'isRented': return compare(+a.isRented, +b.isRented, isAsc);
        case 'dailyPrice': return compare(+a.dailyPrice, +b.dailyPrice, isAsc);
        case 'fuelType': return compare(+a.fuelType, +b.fuelType, isAsc);

        default: return 0;
      }
    });
  }


}

/** Simple sort comparator for example ID/Name columns (for client-side sorting). */
function compare(a: string | number, b: string | number, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}


