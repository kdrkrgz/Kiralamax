import { DataSource } from "@angular/cdk/collections";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { map } from 'rxjs/operators';
import { Observable, of as observableOf, merge } from 'rxjs';
import { UserData } from "src/app/Models/UserData";

export class UserDataSource extends DataSource<UserData> {
    
    data: UserData[];
    paginator!: MatPaginator;
    sort!: MatSort;
  
    constructor(private userData: UserData[]) {
      super();
      // carService.GetCars().subscribe(cars => {
      //   this.data = cars as Car[]
      // })
      this.data = userData as UserData[];
    }
  
    /**
     * Connect this data source to the table. The table will only update when
     * the returned stream emits new items.
     * @returns A stream of the items to be rendered.
     */
    connect(): Observable<UserData[]> {
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
    private getPagedData(data: UserData[]) {
      const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
      return data.splice(startIndex, this.paginator.pageSize);
    }
  
    /**
     * Sort the data (client-side). If you're using server-side sorting,
     * this would be replaced by requesting the appropriate data from the server.
     */
    private getSortedData(data: UserData[]) {
      if (!this.sort.active || this.sort.direction === '') {
        return data;
      }
  
      return data.sort((a, b) => {
        const isAsc = this.sort.direction === 'asc';
        switch (this.sort.active) {
          case 'email': return compare(a.email, b.email, isAsc);
          case 'firstName': return compare(a.firstName, b.firstName, isAsc);
          case 'lastName': return compare(+a.lastName, +b.lastName, isAsc);
          case 'companyName': return compare(+a.companyName, +b.companyName, isAsc);
          case 'customerId': return compare(+a.customerId, +b.customerId, isAsc);
          case 'status': return compare(+a.status, +b.status, isAsc);
          default: return 0;
        }
      });
    }
  }
  
  /** Simple sort comparator for example ID/Name columns (for client-side sorting). */
  function compare(a: string | number, b: string | number, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }
  