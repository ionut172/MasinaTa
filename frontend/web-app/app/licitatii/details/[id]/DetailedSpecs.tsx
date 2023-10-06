'use client';

import {Licitatii} from "@/types";
import {Table} from "flowbite-react";

type Props = {
    licitatii: Licitatii
}
export default function DetailedSpecs({licitatii}: Props) {
    return (
        <Table striped={true}>
            <Table.Body className="divide-y">
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Vanzator
                    </Table.Cell>
                    <Table.Cell>
                        {licitatii.vanzator}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Marca
                    </Table.Cell>
                    <Table.Cell>
                        {licitatii.make}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Model
                    </Table.Cell>
                    <Table.Cell>
                        {licitatii.modelMasina}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        An fabricatie
                    </Table.Cell>
                    <Table.Cell>
                        {licitatii.an}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Kilometraj
                    </Table.Cell>
                    <Table.Cell>
                        {licitatii.kilometraj}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                       Are un pret de rezervare
                    </Table.Cell>
                    <Table.Cell>
                        {licitatii.pretRezervare > 0 ? 'Yes' : 'No'}
                    </Table.Cell>
                </Table.Row>
            </Table.Body>
        </Table>
    );
}