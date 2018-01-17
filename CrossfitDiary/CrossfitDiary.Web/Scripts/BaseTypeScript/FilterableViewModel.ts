module General {
    export class FilterableViewModel {

        filter: KnockoutObservable<any>;
        filteredItems: KnockoutComputed<any>;
        columns: KnockoutObservableArray<{ key; value }>;

        constructor() {
            this.filter = ko.observable();
        }

        initiateFiltering(itemsToSort: KnockoutObservableArray<any>, columnsToObserve: [any]) {
            this.filteredItems = ko.computed(() => {
                var filter = this.filter();
                if (!filter) {
                    return itemsToSort();
                } else {
                    return ko.utils.arrayFilter(itemsToSort(),
                        item => {
                            var matching = -1;
                            ko.utils.arrayForEach(columnsToObserve,
                                c => {
                                    var val = item[c.value];
                                    matching += val().toString().toLowerCase().indexOf(filter.toLowerCase()) + 1;
                                });
                            return matching >= 0;
                        });
                }
            });
        }
    }
}