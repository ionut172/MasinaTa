'use client'
import { Button, TextInput } from "flowbite-react";
import React, { useEffect } from "react";
import { FieldValue, FieldValues, useForm } from "react-hook-form";
import Input from "../components/Input";
import DateInput from "../components/DateInput";
import { createLicitatii, updateLicitatii } from "../actions/licitatiiActions";
import { usePathname, useRouter } from "next/navigation";
import toast from "react-hot-toast";
import { Licitatii } from "@/types";
import path from "path";
type Props = {
    licitatii?: Licitatii
}

export default function LicitatiiForm({licitatii}: Props) {
    const router = useRouter();
    const pathname = usePathname();
    const { control, handleSubmit, setFocus, reset,
        formState: { isSubmitting, isValid } } = useForm({
            mode: 'onTouched'
        });

    useEffect(() => {
        if (licitatii) {
            const { make, modelMasina, culoare, kilometraj, an } = licitatii;
            reset({ make, modelMasina, culoare, kilometraj, an });
        }
        setFocus('make');
    }, [setFocus, reset, licitatii])

    async function onSubmit(data: FieldValues) {
        try {
            let id = '';
            let res;
            if (pathname === '/licitatii/create') {
                res = await createLicitatii(data);
                id = res.id;
            } else {
                if (licitatii) {
                    res = await updateLicitatii(data, licitatii.id);
                    id = licitatii.id;
                }
            }
            if (res.error) {
                throw res.error;
            }
            router.push(`/licitatii/details/${id}`)
        } catch (error: any) {
            toast.error(error.status + ' ' + error.message)
        }
    }
    return (
        <form className="flex-col-mt-3" onSubmit={handleSubmit(onSubmit)}>
            <div className="mb-3 block">
            <Input label="Marca" name="make" control={control} rules={{required: "Marca este necesara"}}/>
            <Input label="Model" name="modelMasina" control={control} rules={{required: "Modelul este necesar"}}/>
            <Input label="Culoare" name="culoare" control={control} rules={{required: "Culoare este necesar"}}/>
            <div className="grid grid-cols-2 gap-3">
            <Input label="An" name="an" control={control} type="number" rules={{required: "Anul este necesar"}}/>
            <Input label="Kilometraj" name="kilometraj" type="number" control={control} rules={{required: "Kilometrajul este necesar"}}/>
            </div>
            {pathname ==='/licitatii/create' && 
            <>
            <Input label="Imagine URL" name="imagineUrl" control={control} rules={{required: "Imaginea este necesara"}}/>
            <div className="grid grid-cols-2 gap-3">
            <Input label="Pret Rezervare" name="pretRezervare" control={control} type="number" rules={{required: "Pret rezervare este necesar"}}/>
            <DateInput label="Licitatie data terminare" dateFormat='dd MMMM yyyy h:mm a' showTimeSelect name="licitatieEnd" type="date" control={control} rules={{required: "Data de finalizare a licitatiei este necesar"}}/>
            </div>
            </>
            }
            </div>
            
            <div className="flex justify-between">
                <Button outline color="gray">Anuleaza</Button>
                <Button isProcessing={isSubmitting} disabled={!isValid} type="submit" outline color="gray">Trimite</Button>
            </div>

        </form>

    )
}