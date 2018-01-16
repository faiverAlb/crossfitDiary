class HomePageController {
    allWorkouts:any;

    constructor(public parameters: { viewModel: { allWorkouts } }) {
        this.allWorkouts = this.parameters.viewModel.allWorkouts;
    }

    private removeWorkout = (data) => {
        debugger;
    }
};
