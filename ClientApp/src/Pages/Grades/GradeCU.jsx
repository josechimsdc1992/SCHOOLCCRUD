import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { apiGrade, apiListGradeDeleteStudent, apiListTeacher } from "../../utils/ApiConfig";
import axios from "axios";
import { Bounce, toast, ToastContainer } from "react-toastify";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { Badge } from "primereact/badge";
import { Dropdown } from "primereact/dropdown";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { ConfirmDialog } from "primereact/confirmdialog";

const getEditarForm = () => ({
    idGrade: { value: 0, error: false },
    name: { value: '', error: false },
    idTeacher: { value: '', error: false }
});

function GradeCU(){
    const navigate = useNavigate();
    let param = useParams();

    const [form, setForm] = useState(getEditarForm());
    const [teachers, setTeachers] = useState([]);
    const [gradeStudents, setGradeStudents] = useState(null);
    const [hasError,setHasError]=useState(false);
    const [visible, setVisible] = useState(null);
    
    useEffect(() => {

        axios.get(`${apiListTeacher}`).then((response) => {
            setTeachers(response.data.result);
            });
        

        if(param.id!=0){
            loadData()
        }
        
        
    }, []);

    const loadData=()=>{
        axios.get(`${apiGrade}/${param.id}`).then((response) => {
            if(!response.data.hasError){
                loadForm(response.data.result)
                setGradeStudents(response.data.result.studentGrades)
            }else{
                console.log(response.data.mensaje)
            }
            
            });
    }

    const handleBack=()=>{
        navigate(-1)
    }

    const handleSave=()=>{

        handleValidate()

        if(hasError)
            return;

        const grade=
        {
            "idGrade": form.idGrade.value,
            "name": form.name.value,
            "idTeacher": form.idTeacher.value
        }

        
        axios
        .post(`${apiGrade}`, grade)
        .then((response) => {
            if(!response.data.hasError){
                console.log(response.data)
                loadForm(response.data.result)
                alertSuccess('Data saved')
            }else{
                console.log(response.data.mensaje)
            }
            
        });
        
    }

    const handleValidate=()=>{
        setHasError(false)

        if(form.name.value==""){
            setHasError(true)
            form.name.error=true;
        }
        if(form.idTeacher.value==""){
            setHasError(true)
            form.idTeacher.error=true;
        }
    }

    const alertSuccess= async (text) => {
        toast.success(text, {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
            transition: Bounce,
            }
            );
        
    };

    const handleChangeForm = async (e) => {
        let error=false
        if(e.target.value==""){
            error=true
        }
        setForm({
            ...form,
            [e.target.name]: {
              value: e.target.value,
              error: error,
            },
          });
        
    };

    const loadForm=(result)=>{
        setForm({
            idGrade: { value: result.idGrade, error: false },
            idTeacher: { value: result.idTeacher, error: false },
            name: { value: result.name, error: false },
            
        })
    }
    const handleDelete=(row)=>{
        const gradeStudient={
            "idGrade": row.idGrade,
            "idStudent": row.idStudent,
            "grupo": row.grupo
          };
        axios
        .post(`${apiListGradeDeleteStudent}`, gradeStudient)
        .then((response) => {
            if(!response.data.hasError){
                console.log(response.data)
                loadData()
                alertSuccess('Data saved')
            }else{
                console.log(response.data.mensaje)
            }
            
        });
    }
    const handleEdit=(row)=>{
        if(row){
            navigate(`/grade/${row.idGrade}/${row.idStudent}`);
        }
    }
    const handleNewStudent=()=>{
        if(form.idGrade.value){
            navigate(`/grade/${form.idGrade.value}/0`);
        }
        
    }

    return (
                <section className="page-section" id="services">
                            <div className="container">
                                <div className="text-center">
                                    <h2 className="section-heading text-uppercase">{param.id==0?'Add Grade':'Edit Grade'}</h2>
                                    <h3 className="section-subheading text-muted">Lorem ipsum dolor sit amet consectetur.</h3>
                                </div>
                                <div className="row text-center">
                                <div className="col-md-6 text-left">
                                <Button onClick={() => handleBack()} label="Back" severity="info" />
                                        
                                </div>
                                <div className="col-md-6 text-right">
                                <Button onClick={() => handleSave()} label="Save" severity="success" />
                                        
                                </div>
                                <div className="flex flex-column gap-2">
                                    <label htmlFor="name">Name</label>
                                    <InputText id="name" name="name"  value={form.name.value}  onChange={(e) => handleChangeForm(e)}/>
                                    { form.name.error ? <Badge value="Required" severity="danger"></Badge> :<></> }
                                    
                                </div>
                               
                                {teachers?<div className="flex flex-column gap-2">
                                    <label htmlFor="idTeacher">Teacher</label>
                                    <Dropdown filter optionValue="idTeacher" optionLabel="fullName" name="idTeacher" value={form.idTeacher.value} options={teachers} onChange={(e) => handleChangeForm(e)} placeholder="Select a Teacher"/>
                                    { form.idTeacher.error ? <Badge value="Required" severity="danger"></Badge> :<></> }
                                </div>:<></>}
                                <div className="col-md-4 text-left">
                                    {form.idGrade.value!=0?<Button onClick={() => handleNewStudent()} label="New" severity="success" />:<></>}
                                
                                        
                                </div>
                                { gradeStudents ? 
                                                                <DataTable value={gradeStudents} paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} selectionMode="single" tableStyle={{ minWidth: '60rem' }}>
                                                                <Column field="grupo" header="Name Group"></Column>
                                                                <Column header="Student Name" body={(row) => (
                                                                    <>
                                                                    <label>{row.student.name} {row.student.surName}</label>
                                                                    </>
                                                                    )}>
                                                                </Column>
                                                                <Column header="" body={(row) => (
                                                                    <>
                                                                    <ConfirmDialog group="declarative"  visible={visible} onHide={() => setVisible(false)} message="Are you sure you want to delete?" 
                                                                        header="Confirmation" icon="pi pi-exclamation-triangle" accept={() => handleDelete(row) }  />
                                                                    <Button onClick={() => handleEdit(row)} icon="pi pi-check" label="Edit" severity="warning"/>
                                                                    <Button onClick={() => setVisible(true)} icon="pi pi-check" label="Delete" severity="danger"/>
                                                                    </>
                                                                    )}>
                                                                </Column>
                                                                </DataTable>
                                                                : <div></div>}
        
                                </div>
                            </div>
                            <ToastContainer />
                    </section>
              )

}
export default GradeCU