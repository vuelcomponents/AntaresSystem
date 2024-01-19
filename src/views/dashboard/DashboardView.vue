<template>
  <main style="min-height:100vh;" :class="['dashboard-container', 'dashboard-container-unique']" id="dashboard-container"  ref="container">
      <section  class="left-dashboard-unique" id="left-dashboard-unique">
        <SideMenu ></SideMenu>
      </section>
      <section class="right-dashboard-unique" >
        <router-view/>
      </section>
 </main>
</template>

<script>
import SideMenu from "@/components/SideMenu.vue";

export default {
    name: "DashboardView",
    components: {SideMenu},
    data(){
      return {

      }
    },
    mounted(){

    },
    methods:{
        hideMenu(data){
            //
        }
    }

}
</script>

<style>
*{

}

</style>