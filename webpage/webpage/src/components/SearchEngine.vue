<script>
import {searchDropdownOptions} from "@/sources/search-dropdown-options";
export default {
  name: "SearchEngine",
  data(){
    return{
      phrase:"",
      place:"",
      distance:null,
      searchDropdownOptions
    }
  }
}
</script>

<template>
  <main class="search-engine-container-container">
  <main class="search-engine-container">
    <div class="search-engine">
    <span class="p-input-icon-left">
        <i class="mdi mdi-magnify" />
        <InputText class="search-input s-text" v-model="phrase" placeholder="Search phrase" />
    </span>
      <span class="p-input-icon-left">
        <i class="mdi mdi-navigation-variant-outline" />
        <InputText class="search-input s-text" v-model="place" placeholder="Place" />
     </span>
      <span class="p-input-icon-left">
        <DropDown class="search-input" v-model="distance" placeholder="Distance" :options="searchDropdownOptions" :optionLabel="(params)=>params.label" />
     </span>
      <span class="p-input-icon-left">
        <Button icon="mdi mdi-send-circle-outline" label="Search"/>
      </span>
    </div>
  </main>
  </main>
</template>

<style scoped lang="scss">

</style>