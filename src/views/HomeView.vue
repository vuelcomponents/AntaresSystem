<template>
    <MesSage :icon="this.message?.icon"  v-if="this.message" :closable="false"  severity="info" style="position:fixed;">{{ this.message?.text }}</MesSage>
    <main ref="tiles" style="transition: transform 0.4s ease;position:absolute;z-index:3; width:100vw; height:100vh; display:flex; justify-content:center; align-items:center">
        <section class="module-splitter" style="grid-gap:20px; display:grid; width:50vw; height:60vh; margin-top:-200px; grid-template-columns:1fr 1fr;text-align:left ">
            <div @click="this.$router.push({name:'Management'})" @mouseenter="onModuleMouseEnter('Data Management', 'mdi mdi-database')" @mouseleave="this.message = null">
                <i class="mdi mdi-data-matrix bigicon"></i>
            </div>
            <div @click="this.$router.push({name:'Recruitment'})"  @mouseenter="onModuleMouseEnter('Bonus System', 'mdi mdi-cash')" @mouseleave="this.message = null">
                <i class="mdi mdi-bank-transfer bigicon"></i>
            </div>
            <div  @mouseenter="onModuleMouseEnter('Operations', 'mdi mdi-account-hard-hat')" @mouseleave="this.message = null">
                <i class="mdi mdi-account-hard-hat bigicon"></i>
            </div>
            <div @click="this.$router.push({name:'Plan'})"  @mouseenter="onModuleMouseEnter('Unknown', 'mdi mdi-domain')" @mouseleave="this.message = null">
                <i class="mdi mdi-sitemap-outline bigicon"></i>
            </div>
        </section>
    </main>


</template>

<script>
import App from '@/App.vue'
import MesSage from "primevue/message";

export default {
  name: 'HomeView',
  components: {
      MesSage

  },
    extends:{App},
    methods:{
        onModuleMouseEnter(text, icon){
            if(!this.message){
                this.message = {}
            }
            this.message.text = text;
            this.message.icon = icon;
        }

    },
    data(){
      return{
          message:null,
      }
    },
    mounted(){
      this.$refs.tiles.style.transform="scale(0.1)"
        setTimeout(()=>{
            this.$refs.tiles.style.transform="scale(1)"
        },20)
    }

}
</script>
