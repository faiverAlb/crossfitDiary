module General {
  export class BaseService {
//    constructPagingUrl = (url: string, serverProcessedViewModel: ServerProcessedViewModel): string => {
//      let trimmed = url.replace(/\/+$/, "");
//      url = trimmed.indexOf("?") >= 0 ? `${url}&` : `${url}?`;
//      url = `${url}searchingWord=${serverProcessedViewModel.searchingWord}&pageIndex=${
//        serverProcessedViewModel.pageIndex}&pageSize=${serverProcessedViewModel.pageSize}&orderByAsc=${
//        serverProcessedViewModel.orderByAsc}`;
//      if (serverProcessedViewModel.propertyToSortBy != null) {
//        url = `${url}&propertyToSortBy=${serverProcessedViewModel.propertyToSortBy}`;
//      }
//      return url;
//    }

//    // Make a GET call to the service
//    protected get<T>(url: string): Q.Promise<T> {
//      return Q.Promise<T>((resolve, reject) => {
//        $.ajax({
//          url: url,
//          success: (data) => resolve(data),
//          method: "GET",
//          error: (e) => reject(e)
//        });
//      });
//    }
//
//    // Make a POST call to the service
//    protected post<T>(url: string, model: any) {
//      return Q.Promise<T>((resolve, reject) => {
//        return $.ajax({
//          url: url,
//          data: model,
//          method: "POST",
//          success: (data) => resolve(data),
//          error: (e) => reject(e)
//        });
//      });
//    }
//
//    //TODO: Merge with usual post
//    protected postExtended<T>(url: string, model: any, contentType: any, processData: boolean) {
//      return Q.Promise<T>((resolve, reject) => {
//        return $.ajax({
//          url: url,
//          type: "POST",
//          data: model,
//          contentType: contentType,
//          processData: processData,
//          method: "POST",
//          success: (data) => resolve(data),
//          error: (e) => reject(e)
//        });
//      });
//    }
//
//    // Make a DELETE call to the service
//    protected delete<T>(url: string) {
//      return Q.Promise((resolve, reject) => {
//        return $.ajax({
//          url: url,
//          method: "DELETE",
//          success: (data) => resolve(data),
//          error: (e) => reject(e)
//        });
//      });
//    }
  }
}