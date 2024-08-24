<script setup>
    import { ref, computed, onMounted, watch } from 'vue'
    import '@/plugins/axios'
    import '@/plugins/notification'
    import { Clone } from '@/plugins/helper'

    const props = defineProps(['item'])
    const emit = defineEmits(['onSaved'])

    const downloadProfileImageUrl = ref()
    const allTitles = ref()
    const allStatusTypes = ref()
    const allCustomerTypes = ref([{ id: 1, name: 'Corporate' }, { id: 2, name: 'Personel' }])

    onMounted(() => {
        axios.get('/Titles').then(result => allTitles.value = result.data)
        //axios.get('/StatusTypes').then(result => allStatusTypes.value = result.data)
    })

    const isValid = computed(() => {
        return props.item.companyName != null &&
            props.item.companyName != '' &&
            props.item.titleId > 0
    })

    watch(() => props.item && props.item.profileImageUrl, (fileName) => {
        if (fileName) downloadProfileImageUrl.value = getUploadFilePath(fileName);
        else downloadProfileImageUrl.value = getUploadFilePath("default.jpg");
    })

    function saveItem() {
        if (!isValid.value) {
            toastr.warning('Form data is not valid!')
            return;
        }

        const item = Clone(props.item);
        if (item.id > 0) {
            axios.put("/Customers/" + item.id, item).then(res => {
                toastr.success("Customer updated!", "Updated")
                emit("onSaved", item)
            })
        } else {
            axios.post("/Customers", item).then(res => {
                toastr.success("Customer created!", "Created")
                emit("onSaved", item)
            })
        }
    }

    function handleFileUpload(event) {
        let selectedFile = event.target.files[0]

        const formData = new FormData()
        formData.append("CustomerId", props.item.id)
        formData.append("ProfileImage", selectedFile)

        axios.post("/Customers/uploadprofileimage", formData).then(res => {
            toastr.success("Uploaded!", "Created")
            downloadProfileImageUrl.value = getUploadFilePath(res.data);
        })
    }

    function getUploadFilePath(fileName) {
        return axios.defaults.baseURL + '/Customers/downloadprofileImage?fileName=' + fileName
    }
</script>
<template>
    <div class="modal modal-blur fade" tabindex="-1" role="dialog" id="appModal">
        <div class="modal-dialog">
            <div class="modal-content" v-if="item">
                <form @submit.prevent="true">
                    <div class="modal-header">
                        <h5 class="modal-title" v-text="item.id > 0 ? 'Edit' : 'New'"></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <input type="hidden" v-model="item.id" />
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Company Name</label>
                            <div class="col-lg-9">
                                <input v-model="item.companyName" class="form-control" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Birth Date</label>
                            <div class="col-lg-9">
                                <Calendar v-model="item.birthDate" dateFormat="dd.mm.yy" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Title</label>
                            <div class="col-lg-9">
                                <Dropdown v-model="item.titleId" :options="allTitles" optionLabel="name" optionValue="id" placeholder="Please select" showClear />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Customer Type</label>
                            <div class="col-lg-9">
                                <select v-model="item.customerType" class="form-select">
                                    <option value="">Please select</option>
                                    <option v-for="customerType in allCustomerTypes" :value="customerType.id">{{ customerType.name }}</option>
                                </select>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Status Type</label>
                            <div class="col-lg-9">
                                <select v-model="item.statusTypeId" class="form-select">
                                    <option value="">Please select</option>
                                    <option v-for="statusType in allStatusTypes" :value="statusType.id">{{ statusType.name }}</option>
                                </select>
                            </div>
                        </div>

                        <div class="row mb-3" v-if="item.id > 0">
                            <label class="col-lg-3 col-form-label">Profile Image</label>
                            <div class="col-lg-5">
                                <input type="file" class="form-control" @change="handleFileUpload" />
                            </div>
                            <div class="col-lg-4" v-show="downloadProfileImageUrl">
                                <img :src="downloadProfileImageUrl" class="img-thumbnail img-fluid" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <button class="btn btn-primary"
                                v-text="item.id > 0 ? 'Update' : 'Save'"
                                @click="saveItem"
                                :disabled="!isValid"></button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>