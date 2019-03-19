import Deserializable = General.Deserializable;
export class PersonMaximumViewModel implements Deserializable {
  exerciseId: number;
  maximumWeight?: number;
  maximumAlternativeWeight?: number;

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
