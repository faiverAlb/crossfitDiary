import Deserializable = General.Deserializable;
export class LeaderboardItemViewModel implements Deserializable {
  level: string;
  userName: string;
  result: string;

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
