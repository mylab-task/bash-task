# MyLab.BashTask

Ознакомьтесь с последними изменениями в [журнале изменений](/CHANGELOG.md).

## Обзор

Разработано на базе [MyLab.TaskApp](https://github.com/mylab-task/task-app). Выполняет `shell` скрипт по `http` запросу с помощью оболочки `bash`.

Запрос для запуска скрипта:

```http
POST /processing
```

## Развёртывание

Предусмотрено развёртывание сервиса в `docker`-контейнере.

Файл запускаемого скрипта необходимо примонтировать в контейнер по адресу `/script.sh`.

Пример `docker-compose.yml`:

```yaml
version: '3.3'

services:
  bash-task-test:
    image: ghcr.io/mylab-task/bash-task
    container_name: bash-task-test
    volumes:
    - ./test-script.sh:/script.sh
```

