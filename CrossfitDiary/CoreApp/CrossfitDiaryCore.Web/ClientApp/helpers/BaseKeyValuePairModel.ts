module General {
  export class BaseKeyValuePairModel<TKey, TValue> {
    constructor(public id: TKey, public text: TValue) {
    }
  }
}