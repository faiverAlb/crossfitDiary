<template>
    <div>
        <div class="dropdown show d-inline">
            <a class="btn btn-secondary text-light  d-inline-block p-0" id="dropdownMenuForTimeLink" href="#" data-target="#navbarContent" aria-controls="navbarContent" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <div class="small-action-link-button">
                    <div class="icon-container">
                        <font-awesome-icon :icon="['fas','clock']"></font-awesome-icon>
                    </div>
                    <div class="text-container">
                        <span data-bind="text:selectedForTimeText">FT</span>
                        <font-awesome-icon :icon="['fas','caret-down']"></font-awesome-icon>
                    </div>
                </div>
            </a>
            <div class="dropdown-menu custom-action-menu-dropdown" aria-labelledby="dropdownMenuForTimeLink">
                <a class="btn btn-secondary text-light  d-inline-block p-0" href="#" role="button">
                    <div class="small-action-link-button">
                        <div class="icon-container">
                            <font-awesome-icon :icon="['fas','clock']"></font-awesome-icon>
                        </div>
                        <div class="text-container">FT*n</div>
                    </div>
                </a>

            </div>
        </div>

        <a class="btn btn-secondary text-light d-inline-block p-0" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false">
            <div class="small-action-link-button">
                <div class="icon-container">
                    <i class="fas fa-list-ol "></i>
                </div>
                <div class="text-container">AMRAP</div>
            </div>
        </a>

        <div class="dropdown show d-inline">
            <a class="btn btn-secondary text-light d-inline-block p-0" id="dropdownMenuEmomLink" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <div class="small-action-link-button">
                    <div class="icon-container">
                        <i class="fas fa-retweet"></i>
                    </div>
                    <div class="text-container">
                        <span data-bind="text:selectedEmomText"></span>
                        <i class="fas fa-caret-down"></i>
                    </div>
                </div>
            </a>

            <div class="dropdown-menu custom-action-menu-dropdown" aria-labelledby="dropdownMenuEmomLink">
                <a class="btn btn-secondary text-light d-inline-block p-0" href="#" role="button">
                    <div class="small-action-link-button">
                        <div class="icon-container">
                            <i class="fas fa-retweet"></i>
                        </div>
                        <div class="text-container">E2MOM</div>
                    </div>
                </a>

            </div>
        </div>

        <a class="btn btn-secondary text-light d-inline-block p-0" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false">
            <div class="small-action-link-button">
                <div class="icon-container">
                    <i class="far fa-pause-circle "></i>
                </div>
                <div class="text-container">NFT</div>
            </div>
        </a>

    </div>
</template>

<script lang="ts">
/* Font awesome icons */
import { faClock } from "@fortawesome/free-solid-svg-icons/faClock";
import { faCaretDown } from "@fortawesome/free-solid-svg-icons/faCaretDown";
import { library } from "@fortawesome/fontawesome-svg-core";

library.add(faClock, faCaretDown);
/**/

import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { Vue, Component, Prop } from "vue-property-decorator";

@Component({
  components: { FontAwesomeIcon }
})
export default class WorkoutsNavigationComponent extends Vue {}
</script>

<style>
</style>