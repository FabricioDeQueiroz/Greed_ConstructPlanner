const timelineInterop = {
    timeline: null,

    initialize: function (element) {
        if (!element) {
            console.error('Container da timeline não encontrado');
            return;
        }

        try {
            // Configurações básicas da timeline
            const options = {
                stack: false, // Desativa o empilhamento automático
                horizontalScroll: true,
                verticalScroll: true,
                zoomKey: 'ctrlKey',
                orientation: 'top',
                height: '400px',
                zoomMin: 1000 * 60 * 60 * 24, // Um dia em milissegundos
                zoomMax: 1000 * 60 * 60 * 24 * 31 * 6, // Três meses em milissegundos
                timeAxis: { scale: 'day', step: 1 }, // Mostra escala em dias
                showCurrentTime: false, // Desativa a linha vertical do tempo atual
                format: {
                    minorLabels: {
                        day: 'D',
                    },
                    majorLabels: {
                        day: 'MMMM',
                        month: 'YYYY'
                    }
                }
            };

            // Cria a timeline
            this.timeline = new vis.Timeline(element, [], [], options);
            console.log('Timeline criada com sucesso');
        } catch (error) {
            console.error('Erro ao criar timeline:', error);
        }
    },

    updateData: function (data) {
        if (!this.timeline) {
            console.error('Timeline não inicializada');
            return;
        }

        try {
            // Cria os datasets
            const items = new vis.DataSet();
            const groups = new vis.DataSet();

            // Ordena os agendamentos pelo atraso (menor para maior)
            const sortedData = [...data].sort((a, b) => a.maxLateness - b.maxLateness);

            // Processa cada agendamento
            sortedData.forEach((agendamento, index) => {
                const groupId = `obra_${index}`;
                const subgroupId = `${groupId}_etapas`; // Subgrupo para as etapas

                // Adiciona o grupo principal
                groups.add({
                    id: groupId,
                    content: agendamento.obra.name
                });

                // Adiciona as etapas
                [
                    ['Projeto', agendamento.dataInicioProjeto, agendamento.dataInicioFormas],
                    ['Formas', agendamento.dataInicioFormas, agendamento.dataInicioConcretagem],
                    ['Concretagem', agendamento.dataInicioConcretagem, agendamento.dataInicioTransporte],
                    ['Transporte', agendamento.dataInicioTransporte, agendamento.dataInicioMontagem],
                    ['Montagem', agendamento.dataInicioMontagem, agendamento.dataFimMontagem]
                ].forEach(([stage, start, end]) => {
                    items.add({
                        group: groupId,
                        subgroup: subgroupId,
                        content: stage,
                        start: new Date(start),
                        end: new Date(end),
                        style: `background-color: ${agendamento.obra.color}40; border-color: ${agendamento.obra.color}`,
                        subgroupOrder: 1 // Força todas as etapas a ficarem na mesma "sublinha"
                    });
                });

                // Adiciona o deadline como uma caixa colorida
                const deadlineDate = new Date(agendamento.obra.deadline);

                items.add({
                    group: groupId,
                    subgroup: subgroupId,
                    content: 'Deadline',
                    start: deadlineDate,
                    type: 'box',
                    className: 'timeline-deadline-box',
                    style: `background-color: ${agendamento.obra.color}; border-color: ${agendamento.obra.color}; color: white;`,
                    subgroupOrder: 1
                });
            });

            // Atualiza a timeline
            this.timeline.setGroups(groups);
            this.timeline.setItems(items);

            // Encontra o intervalo de datas para ajustar a visualização
            const allDates = items.get().reduce((dates, item) => {
                if (item.start) dates.push(new Date(item.start));
                if (item.end) dates.push(new Date(item.end));
                return dates;
            }, []);

            const minDate = new Date(Math.min(...allDates));
            const maxDate = new Date(Math.max(...allDates));

            // Adiciona uma margem de 2 dias antes e depois
            const marginDays = 2;
            minDate.setDate(minDate.getDate() - marginDays);
            maxDate.setDate(maxDate.getDate() + marginDays);

            this.timeline.setWindow(minDate, maxDate);

            console.log('Timeline atualizada com sucesso');
        } catch (error) {
            console.error('Erro ao atualizar timeline:', error);
        }
    }
};

window.timelineInterop = timelineInterop;