var General;
(function (General) {
    var FilterableViewModel = /** @class */ (function () {
        function FilterableViewModel() {
            this.filter = ko.observable();
        }
        FilterableViewModel.prototype.initiateFiltering = function (itemsToSort, columnsToObserve) {
            var _this = this;
            this.filteredItems = ko.computed(function () {
                var filter = _this.filter();
                if (!filter) {
                    return itemsToSort();
                }
                else {
                    return ko.utils.arrayFilter(itemsToSort(), function (item) {
                        var matching = -1;
                        ko.utils.arrayForEach(columnsToObserve, function (c) {
                            var val = item[c.value];
                            matching += val().toString().toLowerCase().indexOf(filter.toLowerCase()) + 1;
                        });
                        return matching >= 0;
                    });
                }
            });
        };
        return FilterableViewModel;
    }());
    General.FilterableViewModel = FilterableViewModel;
})(General || (General = {}));
//# sourceMappingURL=FilterableViewModel.js.map