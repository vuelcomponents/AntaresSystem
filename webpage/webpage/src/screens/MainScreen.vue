<template>
  <main class="main-screen-container">
    <bar-header/>
    <search-engine/>
    <section class="main-screen-router">
      <Navigation/>
      <section class="router">
        <router-view/>
      </section>
    </section>
  </main>
</template>
<script>
import BarHeader from "@/components/BarHeader.vue";
import SearchEngine from "@/components/SearchEngine.vue";

export default {
  name: "MainScreen",
  components:{BarHeader, SearchEngine},
  data(){
    return{

    }
  },
  mounted(){

  }
}
</script>


<style lang="scss">

</style>