<script setup>

import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
</script>

<template>
  <div :hidden="hide(param)" class="box" v-for="param in object.getInputs('text')" :key="param">
    <p class="form-label">
      {{object.getHeaders([param],"first")}}
    </p>
    <InputText  :disabled="lock(param)" v-model="object[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in object.getInputs('numeric')" :key="param" :hidden="hide(param)">
    <p class="form-label">
      {{object.getHeaders([param],"first")}} <span class="mini"> | Number </span>
    </p>
    <InputNumber  :disabled="lock(param)"  :placeholder="object.getHeaders([param],'first')" class="form-text-input" v-model="object[param]"></InputNumber>
  </div>
  <div class="box" v-for="param in object.getInputs('dates')" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{object.getHeaders([param],"first")}} <span class="mini"> | Date </span>
    </p>
    <ParsingCalendar :disabled="lock(param)" :model="object[param]" :key="object[param]" :setter="(value)=>object.setParam(param, value)" />
  </div>
  <div class="box" v-for="param in object.getInputs('collections')" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{object.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
    </p>
    <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="object[param]"></InputClick>
    <div v-if="life.collectionKey === param">
      <CollectionManager :modules="[
                                   object.create.includes(param) ? 'create' : '',
                                   object.select.includes(param) ? 'select' : '',
                                   object.delete.includes(param) ? 'delete' : '',
                                   object.update.includes(param) ? 'update' :'',
                              ]"
                         :service="services[param]"
                         :owner="object"
                         owner-key="object"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="object[param]"></CollectionManager>
    </div>
  </div>
  <div class="box" v-for="param in object.getInputs('objects')" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{object.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <SelectionManager :key="object" :param="param" :disabled="lock(param)" :model="object"></SelectionManager>
    </div>
  </div>
  <div v-for="param in object.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
    <p class="form-label">
      {{object.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                  :disabled="lock(param)"
                  :chooseLabel="object.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                  :maxFileSize="50000000"  @input="object.setFile($event, param)" />
    </div>
  </div>
</template>

<style scoped>

</style>