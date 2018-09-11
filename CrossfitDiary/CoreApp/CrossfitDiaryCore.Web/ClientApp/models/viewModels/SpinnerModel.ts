export class SpinnerModel {
  public size: number;
  public status: boolean;
  public color: string;
  public depth: number;
  public rotation: boolean;
  public speed: number;

  constructor(
    status: boolean,
    size: number = 50,
    color: string = "#17a2b8",
    depth = 3,
    rotation = true,
    speed = 0.8
  ) {
    this.status = status;
    this.size = size;
    this.color = color;
    this.depth = depth;
    this.rotation = rotation;
    this.speed = speed;
  }

  public activate() {
    this.status = true;
  }

  public disable() {
    this.status = false;
  }
}
