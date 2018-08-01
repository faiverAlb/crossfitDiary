var Models;
(function (Models) {
    var PersonExerciseRecord = /** @class */ (function () {
        function PersonExerciseRecord(params) {
            if (params == null) {
                return;
            }
            this.personName = params.personName;
            this.maximumWeight = params.maximumWeight;
            this.date = params.date;
            this.workoutTitle = params.workoutTitle;
            this.positionBetweenOthers = params.positionBetweenOthers;
            this.isItMe = params.isItMe;
            this.exerciseId = params.exerciseId;
            this.maximumWeightValue = params.maximumWeightValue;
        }
        PersonExerciseRecord.prototype.deserialize = function (input) {
            if (input == null) {
                return null;
            }
            return new PersonExerciseRecord({
                personName: input.personName,
                maximumWeight: input.maximumWeight,
                addedToPreviousMaximum: input.addedToPreviousMaximum,
                date: input.date,
                workoutTitle: input.workoutTitle,
                positionBetweenOthers: input.positionBetweenOthers,
                isItMe: input.isItMe,
                exerciseId: input.exerciseId,
                maximumWeightValue: input.maximumWeightValue,
            });
        };
        return PersonExerciseRecord;
    }());
    Models.PersonExerciseRecord = PersonExerciseRecord;
})(Models || (Models = {}));
//# sourceMappingURL=PersonExerciseRecord.js.map