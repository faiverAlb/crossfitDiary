class ServerProcessedViewModel {
    constructor(public searchingWord: string = ""
        , public pageIndex: number = 1
        , public pageSize: number = 15
        , public propertyToSortBy: string = null
        , public orderByAsc: boolean = true) {
    }
}
