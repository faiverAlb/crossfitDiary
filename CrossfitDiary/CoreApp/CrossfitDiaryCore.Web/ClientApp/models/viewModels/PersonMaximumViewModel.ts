import Deserializable = General.Deserializable;
export class PersonMaximumViewModel implements Deserializable {
  exerciseId: number;
  exerciseTitle: string;
  maximumWeight?: number;
  maximumAlternativeWeight?: number;
  calculatedMaximumWeight: number;

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
