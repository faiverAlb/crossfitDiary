module Models {
  export class PersonExerciseRecord {
    personName: string;
    maximumWeight: string;
    date: string;
    workoutTitle: string;
    positionBetweenOthers: number;
    isItMe: boolean;
  }

  export class ObservablePersonExerciseRecord {
    personName: KnockoutObservable<string>;
    maximumWeight: KnockoutObservable<string>;
    date: KnockoutObservable<string>;
    workoutTitle: KnockoutObservable<string>;
    positionBetweenOthers: KnockoutObservable<number>;
    isItMe: KnockoutObservable<boolean>;

    constructor(personName: string,
      maximumWeight: string,
      date: string,
      workoutTitle: string,
      positionBetweenOthers: number,
      isItMe: boolean) {
      this.personName = ko.observable(personName);
      this.maximumWeight = ko.observable(maximumWeight);
      this.date = ko.observable(date);
      this.workoutTitle = ko.observable(workoutTitle);
      this.positionBetweenOthers = ko.observable(positionBetweenOthers);
      this.isItMe = ko.observable(isItMe);
    }
  }
}