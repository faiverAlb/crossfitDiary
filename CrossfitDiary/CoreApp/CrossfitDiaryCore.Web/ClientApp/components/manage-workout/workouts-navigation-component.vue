<template>
    <div>
        <div>
            <b-dropdown variant="link" size="lg" no-caret class="workouts-dropdown">
                <template slot="button-content">
                    <a class="btn btn-secondary text-light  d-inline-block p-0" href="#">
                        <div class="small-action-link-button">
                            <div class="icon-container">
                                <font-awesome-icon :icon="['fas','clock']"></font-awesome-icon>
                            </div>
                            <div class="text-container">
                                <span v-if="$route.path == `/fortime` || $route.path != `/fortimen`">FT</span>
                                <span v-if="$route.path == `/fortimen`">FT*n</span>
                                <font-awesome-icon :icon="['fas','caret-down']"></font-awesome-icon>
                            </div>
                        </div>
                    </a>
                </template>
                <b-dropdown-item>
                    <template>
                        <router-link to="/fortime" class="btn btn-secondary text-light  d-inline-block p-0">
                            <div class="small-action-link-button">
                                <div class="icon-container">
                                    <font-awesome-icon :icon="['fas','clock']"></font-awesome-icon>
                                </div>
                                <div class="text-container">
                                    <span data-bind="text:selectedForTimeText">FT</span>
                                    <font-awesome-icon :icon="['fas','caret-down']"></font-awesome-icon>
                                </div>
                            </div>
                        </router-link>
                    </template>
                </b-dropdown-item>
                <b-dropdown-item>
                    <template>
                        <router-link to="/fortimen" class="btn btn-secondary text-light  d-inline-block p-0" href="#" role="button">
                            <div class="small-action-link-button">
                                <div class="icon-container">
                                    <font-awesome-icon :icon="['fas','clock']"></font-awesome-icon>
                                </div>
                                <div class="text-container">FT*n</div>
                            </div>
                        </router-link>
                    </template>
                </b-dropdown-item>
            </b-dropdown>
        </div>
        <div class="dropdown show d-inline">
            <router-link to="/fortime" class="btn btn-secondary text-light  d-inline-block p-0" id="dropdownMenuForTimeLink" href="#" data-target="#navbarContent" aria-controls="navbarContent" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <div class="small-action-link-button">
                    <div class="icon-container">
                        <font-awesome-icon :icon="['fas','clock']"></font-awesome-icon>
                    </div>
                    <div class="text-container">
                        <span data-bind="text:selectedForTimeText">FT</span>
                        <font-awesome-icon :icon="['fas','caret-down']"></font-awesome-icon>
                    </div>
                </div>
            </router-link>
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

        <router-link to="/amrap" class="btn btn-secondary text-light d-inline-block p-0" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false">
            <div class="small-action-link-button">
                <div class="icon-container">
                    <font-awesome-icon :icon="['fas','list-ol']"></font-awesome-icon>
                </div>
                <div class="text-container">AMRAP</div>
            </div>
        </router-link>

        <div class="dropdown show d-inline">
            <a class="btn btn-secondary text-light d-inline-block p-0" id="dropdownMenuEmomLink" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <div class="small-action-link-button">
                    <div class="icon-container">
                        <font-awesome-icon :icon="['fas','retweet']"></font-awesome-icon>

                    </div>
                    <div class="text-container">
                        <span data-bind="text:selectedEmomText">EMOM</span>
                        <font-awesome-icon :icon="['fas','caret-down']"></font-awesome-icon>
                    </div>
                </div>
            </a>

            <div class="dropdown-menu custom-action-menu-dropdown" aria-labelledby="dropdownMenuEmomLink">
                <a class="btn btn-secondary text-light d-inline-block p-0" href="#" role="button">
                    <div class="small-action-link-button">
                        <div class="icon-container">
                            <font-awesome-icon :icon="['fas','retweet']"></font-awesome-icon>
                        </div>
                        <div class="text-container">E2MOM</div>
                    </div>
                </a>

            </div>
        </div>

        <a class="btn btn-secondary text-light d-inline-block p-0" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false">
            <div class="small-action-link-button">
                <div class="icon-container">
                    <font-awesome-icon :icon="['far','pause-circle']"></font-awesome-icon>
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
import { faListOl } from "@fortawesome/free-solid-svg-icons/faListOl";
import { faRetweet } from "@fortawesome/free-solid-svg-icons/faRetweet";
import { library } from "@fortawesome/fontawesome-svg-core";
import { faPauseCircle } from "@fortawesome/free-regular-svg-icons/faPauseCircle";

library.add(faClock, faCaretDown, faListOl, faRetweet, faPauseCircle);
/**/

import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { Vue, Component, Prop } from "vue-property-decorator";
import bDropdown from "bootstrap-vue/es/components/dropdown/dropdown";
import bDropdownItem from "bootstrap-vue/es/components/dropdown/dropdown-item";
import bDropdownItemButton from "bootstrap-vue/es/components/dropdown/dropdown-item-button";

@Component({
  components: { FontAwesomeIcon, bDropdown, bDropdownItem }
})
export default class WorkoutsNavigationComponent extends Vue {}
</script>

<style>
</style>