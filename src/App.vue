<template>
  <img src="@/assets/refactor/bg.jpg" style="position:fixed;top:-25%;left:0; user-select:none; pointer-events:none;"/>

  <div v-if="loading" class="spinner-container">
  <Spinner></Spinner>
</div>
<header class="web-header _theme" id="web-header" :hidden="this.$route.path.includes('webpage')">
    <div>
        <span id="hide-menu" class="rotable" style="margin-left:15px; color:#ffffff; cursor:pointer;"  @click="hideLeftMenu">
            <i class="mdi mdi-menu"></i>
        </span>
        <span class="rotable" style="margin-left:35px; color:#ffffff; cursor:pointer;">
                <i class="mdi mdi-cash"></i>
            </span>
        <span class="rotable" style="margin-left:15px; color:#ffffff; cursor:pointer;">
                <i class="mdi mdi-database"></i>
            </span>
        <span class="rotable" style="margin-left:15px; color:#ffffff; cursor:pointer;">
                <i class="mdi mdi-account-hard-hat"></i>
            </span>
        <span class="rotable" style="margin-left:15px; color:#ffffff; cursor:pointer;">
                <i class="mdi mdi-domain"></i>
            </span>
    </div>
    <div>
        <MenuBar class="on-dark-bg" :model="menu"></MenuBar>
    </div>
</header>
<section style="margin-top:70px;" :class="{_theme:true}">
    <router-view/>
</section>
<main :hidden="this.$route.path !== '/'" class="_theme">
    <header class="web-header min-height-320" style="padding:0; max-width:100vw; background:none">
       <canvas id="canvas" style="margin:0;max-width:100vw;"></canvas>
    </header>
    <footer :style="`background-size: cover;background-repeat: no-repeat;background-position: center;background:url('${footer}');  user-select: none;position:absolute; min-width:100vw; right:0;left:auto; margin-top:100px;height:400px;`">
</footer>
</main>

<!--  <router-view/>-->
</template>

<script>
import footer from "@/assets/stacked/footer.png";
import waves from '@/assets/stacked/waves.jpg'

import "ag-grid-community/styles/ag-grid.css"; // Core grid CSS, always needed
import "ag-grid-community/styles/ag-theme-alpine.css";
import {inject} from "vue";
import activateStarback from "@/utils/functions/activateStarback";
import activateRotables from "@/utils/functions/activateRotables";
import AppMenu from "@/AppMenu";
import hideLeftMenu from "@/utils/functions/hideLeftMenu";
import Toast from "@/utils/classes/Toast";
import {getTheme} from "@/core/theme";
import prototypes from "@/utils/functions/prototypes";
prototypes()

export default {
    components: {},
    setup(){
        const services = {
        }
        const toast = new Toast();
        const emitter = inject('emitter')
        return {services, emitter, toast};
    },
    mounted(){
      activateRotables();
      this.emitter.on('loading', (state)=>{
         this.loading = state;
      })
      this.emitter.on('error', (message)=>{
        this.toast.error(message)
      })
      this.emitter.on('warn', (message)=>{
        this.toast.warn(message)
      })
      this.$nextTick(getTheme)

    },
    data(){
        return {
            message:null,
            menuHide:false,
            clientWidth:document.body.clientWidth,
            footer,
            waves,
            menu:AppMenu,
            loading:false
        }
    },
    methods:{
        hideLeftMenu,
    },
}

</script>

<style lang="scss">
@import './antares-casual-theme';
@import 'antares-light-theme';
@import 'antares-redshark-theme';

</style>
