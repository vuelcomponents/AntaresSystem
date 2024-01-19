<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in documentType.getInputs('text')" :key="param">
          <p class="form-label">
            {{documentType.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="documentType[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in documentType.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{documentType.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="documentType.getHeaders([param],'first')" class="form-text-input" v-model="documentType[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in documentType.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{documentType.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <Calendar :disabled="lock(param)" date-format="yy-mm-dd"  v-model="documentType[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in documentType.getInputs('collections')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{documentType.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
          </p>
          <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="documentType[param]"></InputClick>
          <div v-if="life.collectionKey === param">
            <CollectionManager :modules="[
                                   documentType.create.includes(param) ? 'create' : '',
                                   documentType.select.includes(param) ? 'select' : '',
                                   documentType.delete.includes(param) ? 'delete' : '',
                                   documentType.update.includes(param) ? 'update' :'',
                              ]"
                               :owner="documentType"
                               owner-key="document"
                               :emitter="emitter"
                               :set-collection="setCollection"
                               :collection-key="param"
                               :collection="documentType[param]"></CollectionManager>
          </div>
        </div>
        <div class="box" v-for="param in documentType.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{documentType.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="documentType" :param="param" :disabled="lock(param)" :model="documentType"></SelectionManager>
          </div>
        </div>
        <div v-for="param in documentType.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
          <p class="form-label">
            {{documentType.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                        :disabled="lock(param)"
                        :chooseLabel="documentType.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                        :maxFileSize="50000000"  @input="documentType.setFile($event, param)" />
          </div>
          <Tag v-if="documentType.fileData"
               severity="warning"
               style="margin-top:4px;cursor:pointer;"
               @click="this.services.doc.download(this.$route.params.id, documentType.fileName)"
          >Download</Tag>
        </div>
      </template>
      <template v-slot:item2contentLeft>
        <div class="box">
          {{ documentType }}
        </div>
      </template>

      <template v-slot:item2contentRight>
        <div class="box">
          right
        </div>
      </template>

      <template v-slot:item3contentLeft>
        <div class="box">
          left
        </div>
      </template>

      <template v-slot:item3contentRight>
        <div class="box">
          right
        </div>
      </template>
    </DataPopup>
  </div>
</template>

<script>

import DataPopup from "@/components/popups/DataPopup.vue";
import { inject } from "vue";
import {DocumentTypeService} from "@/services/DocumentTypeService";
import Tag from "primevue/tag";
import Toast from "@/utils/classes/Toast";
import DocumentTypeCrudController from "@/cruds/DocumentTypeCrudController";
import DocumentTypeClass from "@/views/dashboard/DataView/documentTypes/DocumentTypeClass";
import DocumentTypeLife from "@/views/dashboard/DataView/documentTypes/DocumentTypeLife";
import EditOptions from "@/views/dashboard/DataView/documentTypes/options/EditOptions";



export default {
  components: {DataPopup, Tag},
  setup(){
    const emitter = inject('emitter')
    const services = {
      doc: new DocumentTypeService(emitter),
    };
    const toast =new Toast();
    const crud = new DocumentTypeCrudController(services.doc, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      documentType: new DocumentTypeClass(),
      life: new DocumentTypeLife(),
      popupOptions: new EditOptions(this.$router, this.update),
    }
  },
  async mounted(){
    this.documentType = await this.crud.getById(this.$route.params.id)
  },
  methods:{
    update(){
      this.crud.update(this.documentType).then(res=>{
        if(res?.invalid){
          console.log('invalid document create, empty fields', res.invalid)
        }
        if(res.status === 200) {
          history.back()
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.documentType[key] = collection
      }
    },
    lock(param){
      if(!this.documentType.$lock[param]){
        return false;
      }
      return this.documentType.$lock[param]()
    },
    hide(param){
      if(!this.documentType.$hide[param]){
        return false;
      }
      return this.documentType.$hide[param]()
    }
  }
}
</script>



<style>

</style>