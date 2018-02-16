var ServerProcessedViewModel = (function () {
    function ServerProcessedViewModel(searchingWord, pageIndex, pageSize, propertyToSortBy, orderByAsc) {
        if (searchingWord === void 0) { searchingWord = ""; }
        if (pageIndex === void 0) { pageIndex = 1; }
        if (pageSize === void 0) { pageSize = 15; }
        if (propertyToSortBy === void 0) { propertyToSortBy = null; }
        if (orderByAsc === void 0) { orderByAsc = true; }
        this.searchingWord = searchingWord;
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
        this.propertyToSortBy = propertyToSortBy;
        this.orderByAsc = orderByAsc;
    }
    return ServerProcessedViewModel;
}());
//# sourceMappingURL=ServerProcessedViewModel.js.map