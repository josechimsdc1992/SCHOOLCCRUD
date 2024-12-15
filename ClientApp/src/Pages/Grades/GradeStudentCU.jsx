import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { apiGrade, apiListGradeSaveStudent, apiListStudent } from "../../utils/ApiConfig";
import { Bounce, toast, ToastContainer } from "react-toastify";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { Badge } from "primereact/badge";
import { Dropdown } from "primereact/dropdown";

const getEditarForm = () => ({
    idGrade: { value: 0, error: false },
    idStudent: { value: 0, error: false },
    grupo: { value: '', error: false }
    
});

function GradeStudentCU(){
    const navigate = useNavigate();
    let param = useParams();

    const [form, setForm] = useState(getEditarForm());
    const [hasError,setHasError]=useState(false)
    const [students, setStudents] = useState([]);

    
    useEffect(() => {
        axios.get(`${apiListStudent}`).then((response) => {
            setStudents(response.data.result);
            });
        
            loadData()
    }, []);

    const loadData=()=>{
        if(param.idGrade!=0 && param.idStudent!=0){
            axios.get(`${apiGrade}/${param.idGrade}/${param.idStudent}`).then((response) => {
                if(!response.data.hasError){
                    loadForm(response.data.result)
                }else{
                    console.log(response.data.mensaje)
                }
                
                });
        }else{
            if(param.idGrade!=0){
                setForm({
                    ...form,
                    ['idGrade']: {
                      value: param.idGrade,
                      error: false,
                    },
                  });
            }
        }
        
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
            "grupo": form.grupo.value,
            "idStudent": form.idStudent.value
        }

        axios
        .post(`${apiListGradeSaveStudent}`, grade)
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

        if(form.grupo.value==""){
            setHasError(true)
            form.grupo.error=true;
        }
        if(form.idStudent.value==0){
            setHasError(true)
            form.idStudent.error=true;
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
            idStudent: { value: result.idStudent, error: false },
            grupo: { value: result.grupo, error: false },
        })
    }
    
    return (
                    <section className="page-section" id="services">
                                <div className="container">
                                    <div className="text-center">
                                        <h2 className="section-heading text-uppercase">{param.id==0?'Add Student to Grade':'Edit Grade from student'}</h2>
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
                                        <InputText id="name" name="grupo"  value={form.grupo.value}  onChange={(e) => handleChangeForm(e)}/>
                                        { form.grupo.error ? <Badge value="Required" severity="danger"></Badge> :<></> }
                                        
                                    </div>
                                   
                                    {students?<div className="flex flex-column gap-2">
                                        <label htmlFor="idStudent">Student</label>
                                        <Dropdown filter optionValue="idStudent" optionLabel="fullName" name="idStudent" value={form.idStudent.value} options={students} onChange={(e) => handleChangeForm(e)} placeholder="Select a Student"/>
                                        { form.idStudent.error ? <Badge value="Required" severity="danger"></Badge> :<></> }
                                    </div>:<></>}

                                    </div>
                                </div>
                                <ToastContainer />
                        </section>
                  )
}
export default GradeStudentCU