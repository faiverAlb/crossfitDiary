module General {
  export class BaseController {
  }

  export interface Serializable<T> {
    deserialize(input: any): T;
  }

  export interface Deserializable{
    deserialize(input: any): this;
  }
}