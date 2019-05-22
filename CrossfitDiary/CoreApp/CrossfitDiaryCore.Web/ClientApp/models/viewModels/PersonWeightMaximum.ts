import Deserializable = General.Deserializable;
export class PersonWeightMaximum implements Deserializable {
  exerciseId: number;
  exerciseTitle: string;
  maximumWeight: number;

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
